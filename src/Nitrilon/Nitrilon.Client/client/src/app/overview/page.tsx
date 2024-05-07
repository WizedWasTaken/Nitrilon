"use client";

// Dependencies
import { MembersTable } from "@/components/customDataTable/members";
import { Button } from "@/components/ui/button";
import { ModeToggle } from "@/components/ui/dark-mode-toggle";
import Link from "next/link";

/**
 * Overview page for the members.
 * @returns Jsx.elemnt with the overview of the members
 */
export default function Overview() {
  return (
    <div className="overflow-y-auto p-5">
      {/* Navigation buttons & light mode toggle */}
      <article className="w-full relative flex justify-between top-0">
        <div className="flex gap-5">
          <Button asChild>
            <Link href="/">Rating system</Link>
          </Button>
          <Button asChild>
            <Link href="/admin">Event overblik</Link>
          </Button>
        </div>
        <ModeToggle />
      </article>
      {/* Text for members table */}
      <section className="text-center">
        <h2 className="text-4xl font-bold">Medlemmer</h2>
        <p className="text-muted-foreground">
          Her kan du se en oversigt over alle medlemmer.
        </p>
      </section>
      {/* Members table component */}
      <section>
        <MembersTable />
      </section>
    </div>
  );
}
