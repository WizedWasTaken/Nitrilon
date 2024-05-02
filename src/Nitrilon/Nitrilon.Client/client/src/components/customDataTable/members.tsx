// Use client directive for Next.js
"use client";

// Imports
import React from "react";
import { DataTable } from "@/components/data-table/data-table";
import { MemberTableColumn } from "@/lib/columnDefinitions";
import { Member } from "@/lib/types"; // Ensure you have a Member type defined appropriately
import MembersTableTop from "@/components/customDataTable/membersTableTop";
import { toast } from "sonner";

/*
 * This is the main page for the users section of the admin dashboard.
 * It shows all the users that have been registered.
 * It is possible to delete users from this page.
 */
export function MembersTable() {
  // Assuming Member[] is correctly typed according to your needs
  const [members, setMembers] = React.useState<Member[]>([]);

  React.useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Member/all`)
      .then((res) => res.json())
      .then((data: Member[]) => setMembers(data)); // Ensure the correct typing for data fetched
  }, []);

  const createMember = (
    name: string,
    phoneNumber: string,
    email: string,
    enrollmentDate: Date,
    membershipId: number
  ) => {
    console.log("createMember");
    console.log(name);
    console.log(phoneNumber);
    console.log(email);
    console.log(enrollmentDate);
    console.log(membershipId);
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Member`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        name,
        phoneNumber,
        email,
        enrollmentDate,
        membershipId,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          toast.error("Der skete en fejl", {
            duration: 5000,
          });

          return;
        }

        toast.success("Medlem oprettet", {
          duration: 5000,
        });

        return response.json();
      })
      .then((data: Member) => {
        setMembers((prevMembers) => [...prevMembers, data]);
      });
  };

  // Handler to update member status
  const updateMemberStatus = (memberId: number, isDeleted: boolean) => {
    console.log("27: " + memberId + " " + isDeleted);
    fetch(
      `${process.env.NEXT_PUBLIC_API_URL}/api/Member?memberId=${memberId}`,
      {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ isDeleted: !isDeleted }),
      }
    ).then((response) => {
      if (!response.ok) {
        console.error("Failed to update member status");
      }
      setMembers((prevMembers) => {
        return prevMembers.map((member) => {
          if (member.memberId === memberId) {
            return { ...member, isDeleted: !isDeleted };
          }
          return member;
        });
      });
    });
  };

  return (
    <>
      <MembersTableTop members={members} createNewMember={createMember} />
      <DataTable
        data={members}
        columns={MemberTableColumn(updateMemberStatus)} // Ensure the column definitions are received as expected
      />
    </>
  );
}
