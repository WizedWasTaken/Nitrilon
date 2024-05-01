"use client";

// Important Imports
import { ColumnDef } from "@tanstack/react-table";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {} from "@tanstack/react-table";

import { DataTableColumnHeader } from "@/components/data-table/data-table-header";

// UI
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { MoreHorizontal } from "lucide-react";

// Types
import { Member } from "@/lib/types";

// ! This is an example table column. Copy columns from UserTableColumns and modify them.
export const MemberTableColumn: ColumnDef<Member>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={
          table.getIsAllPageRowsSelected() ||
          (table.getIsSomePageRowsSelected() && "indeterminate")
        }
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "name",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Navn" />
    ),
    cell: ({ row }) => {
      const name: string = row.getValue("name");
      return <div className="text-right font-medium">{name}</div>;
    },
  },
  {
    accessorKey: "phoneNumber",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Telefonnummer" />
    ),
    cell: ({ row }) => {
      let phoneNumber: string = row.getValue("phoneNumber");
      phoneNumber = phoneNumber.replace(
        /(\d{3})(\d{2})(\d{2})(\d{2})(\d{2})/,
        "+$1 $2 $3 $4 $5"
      );
      phoneNumber = phoneNumber.replace("+045", "+45");
      return <div className="text-right">{phoneNumber}</div>;
    },
  },
  {
    accessorKey: "email",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Email" />
    ),
    cell: ({ row }) => {
      const email: string = row.getValue("email");
      return <div className="text-right">{email}</div>;
    },
  },
  {
    accessorKey: "enrollmentDate",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Indmeldelsesdato" />
    ),
    cell: ({ row }) => {
      const enrollmentDate: string = row.getValue("enrollmentDate");
      return (
        <div className="text-right">
          {new Date(enrollmentDate).toLocaleDateString("da-DK")}
        </div>
      );
    },
  },
  {
    accessorKey: "membership.name",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Medlemskab" />
    ),
    cell: ({ row }) => (
      <div className="capitalize text-right">
        {row.original.membership.name}
      </div>
    ),
  },
  {
    accessorKey: "isDeleted",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Indmeldt" />
    ),
    cell: ({ row }) => {
      let isDeleted: boolean = row.renderValue("isDeleted");
      return <div className="text-right">{isDeleted ? "Nej" : "Ja"}</div>;
    },
  },
  {
    id: "actions",
    cell: ({ row }) => {
      const member = row.original;

      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="ghost" className="h-8 w-8 p-0">
              <span className="sr-only">Åben menu</span>
              <MoreHorizontal className="h-4 w-4" />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            <DropdownMenuLabel>Muligheder</DropdownMenuLabel>
            <DropdownMenuItem
              onClick={() =>
                navigator.clipboard.writeText(member.memberId.toString())
              }
            >
              Kopier medlem ID
            </DropdownMenuItem>
            <DropdownMenuSeparator />
            {/* TODO: Add indmeld og udmeld option */}
            {/* TODO: There's a bug with it not updating the text in "isDeleted" when indmelding or outmelding.  */}
            <DropdownMenuItem
              onClick={() => {
                const { memberId, isDeleted } = row.original;
                fetch(
                  `${process.env.NEXT_PUBLIC_API_URL}/api/Member?memberId=${memberId}`,
                  {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ isDeleted: !isDeleted }),
                  }
                ).then((response) => {
                  console.log(row.original.isDeleted);
                  row.original.isDeleted = !isDeleted;
                  console.log(row.original.isDeleted);
                });
              }}
            >
              {row.original.isDeleted ? "Indmeld" : "Udmeld"}
            </DropdownMenuItem>
            <DropdownMenuItem>Se bruger detaljer</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];
