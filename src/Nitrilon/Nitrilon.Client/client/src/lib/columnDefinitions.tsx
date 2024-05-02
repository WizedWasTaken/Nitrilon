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
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
} from "@/components/ui/dropdown-menu";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogDescription,
  DialogTitle,
  DialogHeader,
  DialogFooter,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";

import { DataTableColumnHeader } from "@/components/data-table/data-table-header";

// UI
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { MoreHorizontal } from "lucide-react";

// Types
import { Member, Membership } from "@/lib/types";
import { Label } from "@radix-ui/react-dropdown-menu";
import { useState } from "react";

// ! This is an example table column. Copy columns from UserTableColumns and modify them.
export function MemberTableColumn(
  updateMemberStatus: (memberId: number, isDeleted: boolean) => void,
  updateMember: (member: Member) => void,
  setTempMember: (member: Member) => void
): ColumnDef<Member>[] {
  const [membershipId, setMembershipId] = useState<string>("1");

  return [
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
      enableSorting: false,
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
        const member = row.original as Member;

        return (
          <Dialog>
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button
                  variant="ghost"
                  className="h-8 w-8 p-0"
                  onClick={() => setTempMember(member)}
                >
                  <span className="sr-only">Åben menu</span>
                  <MoreHorizontal className="h-4 w-4" />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuLabel>Muligheder</DropdownMenuLabel>
                <DropdownMenuSeparator />
                {/* TODO: Add indmeld og udmeld option */}
                {/* TODO: There's a bug with it not updating the text in "isDeleted" when indmelding or outmelding.  */}
                <DropdownMenuItem
                  onClick={() => {
                    const { memberId, isDeleted } = row.original;
                    updateMemberStatus(memberId, isDeleted);
                  }}
                >
                  {row.original.isDeleted ? "Indmeld" : "Udmeld"}
                </DropdownMenuItem>
                <DialogTrigger asChild>
                  <DropdownMenuItem>Rediger</DropdownMenuItem>
                </DialogTrigger>
              </DropdownMenuContent>
            </DropdownMenu>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Rediger medlem</DialogTitle>
                <DialogDescription>
                  Rediger medlemmet {member.name}
                </DialogDescription>
              </DialogHeader>
              <form
                onSubmit={(event) => {
                  event.preventDefault();
                  const formData = new FormData(event.currentTarget);
                  const name = formData.get("name") as string;
                  let phoneNumber = formData.get("phoneNumber") as string;
                  if (phoneNumber.length === 8) {
                    phoneNumber = "045" + phoneNumber;
                  } else if (phoneNumber.length === 11) {
                    phoneNumber = phoneNumber.replace("+", "0");
                  }
                  const email = formData.get("email") as string;
                  const enrollmentDate = formData.get(
                    "enrollmentDate"
                  ) as string;
                  const membershipId = formData.get("membershipId") as string;

                  updateMember({
                    ...member,
                    name,
                    phoneNumber,
                    email,
                    enrollmentDate: new Date(enrollmentDate),
                  });
                }}
              >
                <DialogDescription className="flex flex-col gap-5">
                  <div>
                    <Label>Navn</Label>
                    <Input
                      name="name"
                      id="name"
                      type="text"
                      defaultValue={member.name}
                    />
                  </div>{" "}
                  <div>
                    <Label>Telefon nummer</Label>
                    <Input
                      name="phoneNumber"
                      id="phoneNumber"
                      type="text"
                      defaultValue={member.phoneNumber}
                    />
                  </div>{" "}
                  <div>
                    <Label>E-mail</Label>
                    <Input
                      name="email"
                      id="email"
                      type="email"
                      defaultValue={member.email}
                    />
                  </div>{" "}
                  <div>
                    <Label>Tilmeldings dato</Label>
                    <Input
                      name="enrollmentDate"
                      id="enrollmentDate"
                      type="date"
                      defaultValue={
                        new Date(member.enrollmentDate)
                          .toISOString()
                          .split("T")[0]
                      }
                    />
                  </div>
                </DialogDescription>
                <DialogFooter>
                  <Button type="submit">Bekræft</Button>
                </DialogFooter>
              </form>
            </DialogContent>
          </Dialog>
        );
      },
    },
  ];
}
