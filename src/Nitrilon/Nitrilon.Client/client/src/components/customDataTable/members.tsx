// Use client directive for Next.js
"use client";

// Imports
import React, { Dispatch, SetStateAction } from "react";
import { DataTable } from "@/components/data-table/data-table";
import { MemberTableColumn } from "@/lib/columnDefinitions";
import { Member } from "@/lib/types"; // Ensure you have a Member type defined appropriately
import MembersTableTop from "@/components/customDataTable/membersTableTop";
import { toast } from "sonner";
import { Dialog } from "../ui/dialog";

/*
 * This is the main page for the users section of the admin dashboard.
 * It shows all the users that have been registered.
 * It is possible to delete users from this page.
 */
export function MembersTable() {
  // Assuming Member[] is correctly typed according to your needs
  const [members, setMembers] = React.useState<Member[]>([]);
  const [tempMember, setTempMemberState] = React.useState<Member[]>([]);

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
    if (name.length === 0 || phoneNumber.length === 0 || email.length === 0) {
      toast.error("Udfyld alle felter", {
        duration: 5000,
      });

      return;
    }

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

  function setTempMember(member: Member) {
    setTempMemberState((prevTempMember) => {
      return [...prevTempMember, member];
    });
  }

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

  const updateMember = (member: Member) => {
    setTempMemberState((prevTempMember) => {
      return prevTempMember.filter(
        (tempMember) => tempMember.memberId !== member.memberId
      );
    });

    console.log("updateMember");
    console.log(member);
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Member/update`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(member),
    }).then((response) => {
      if (!response.ok) {
        console.error("Failed to update member");
      }
      setMembers((prevMembers) => {
        return prevMembers.map((prevMember) => {
          if (prevMember.memberId === member.memberId) {
            return member;
          }
          return prevMember;
        });
      });
    });
  };

  return (
    <>
      <MembersTableTop members={members} createNewMember={createMember} />
      <DataTable
        data={members}
        columns={MemberTableColumn(
          updateMemberStatus,
          updateMember,
          setTempMember
        )} // Ensure the column definitions are received as expected
      />
    </>
  );
}
