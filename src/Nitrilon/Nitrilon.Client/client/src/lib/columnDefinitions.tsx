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
    enableHiding: false,
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
            <DropdownMenuItem>Se bruger</DropdownMenuItem>
            <DropdownMenuItem>Se bruger detaljer</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      );
    },
  },
];
