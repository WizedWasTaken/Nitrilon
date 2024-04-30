"use client";

// Imports
import React from "react";
import { DataTable } from "@/components/data-table/data-table";
import { MemberTableColumn } from "@/lib/columnDefinitions";

/*
 * This is the main page for the users section of the admin dashboard.
 * It shows all the users that have been registered.
 * It is possible to delete users from this page.
 */
export function MembersTable() {
  const [members, setMembers] = React.useState([]);

  React.useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Member/all`)
      .then((res) => res.json())
      .then((data) => setMembers(data));
  }, []);

  return (
    <>
      <DataTable data={members} columns={MemberTableColumn} />
    </>
  );
}
