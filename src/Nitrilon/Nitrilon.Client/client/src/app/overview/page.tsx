"use client";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Nitrilon - Medlemmer",
  description: "S2 - Nitrilon - Noah A. Nielsen",
};

import { MembersTable } from "@/components/customDataTable/members";

export default function Overview() {
  return (
    <div className="overflow-y-auto p-5">
      <section className="text-center">
        <h2 className="text-xl font-bold">Medlemmer</h2>
        <p className="text-muted-foreground">
          Her kan du se en oversigt over alle medlemmer.
        </p>
      </section>
      <section>
        <MembersTable />
      </section>
    </div>
  );
}
