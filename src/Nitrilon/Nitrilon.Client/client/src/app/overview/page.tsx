"use client";

import { MembersTable } from "@/components/customDataTable/members";
import { ModeToggle } from "@/components/ui/dark-mode-toggle";

export default function Overview() {
  return (
    <div className="overflow-y-auto p-5">
      <div className="absolute w-full flex justify-end px-7">
        <ModeToggle />
      </div>
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
