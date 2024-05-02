"use client";

import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { MixerHorizontalIcon } from "@radix-ui/react-icons";
import { Table } from "@tanstack/react-table";

import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu";
import { Input } from "@/components/ui/input";
import React from "react";
import { CustomColumnDef } from "@/lib/types";

interface DataTableViewOptionsProps<TData> {
  table: Table<TData>;
}

export function DataTableViewOptions<TData>({
  table,
}: DataTableViewOptionsProps<TData>) {
  console.log("Table: ", table.getAllColumns());
  const [sortName, setSortName] = React.useState<string>("name");

  const changeSortName = (name: string) => {
    setSortName(name);
  };

  return (
    <div className="flex flex-row justify-between items-center w-full pb-2">
      <div className="flex w-full max-w-sm items-center space-x-2">
        <Input
          placeholder={`Søg efter ${sortName}`}
          value={(table.getColumn(sortName)?.getFilterValue() as string) ?? ""}
          onChange={(event) =>
            table.getColumn(sortName)?.setFilterValue(event.target.value)
          }
          className="max-w-sm h-10 w-full lg:w-[250px]"
        />
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button
              variant="outline"
              size="lg"
              className="ml-auto hidden h-10 lg:flex"
            >
              <MixerHorizontalIcon className="mr-2 h-4 w-4" />
              Filter
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end" className="w-[180px]">
            <DropdownMenuLabel>Filter</DropdownMenuLabel>
            <DropdownMenuSeparator />
            {table
              .getAllColumns()
              .filter(
                (column) =>
                  typeof column.accessorFn !== "undefined" &&
                  column.getCanSort()
              )
              .map((column) => {
                // @ts-ignore
                const displayName = column.columnDef.meta.name as string;
                return (
                  <DropdownMenuCheckboxItem
                    key={column.id}
                    className="capitalize"
                    checked={column.id === sortName}
                    onCheckedChange={() => changeSortName(column.id)}
                  >
                    {displayName}
                  </DropdownMenuCheckboxItem>
                );
              })}
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button
            variant="outline"
            size="lg"
            className="ml-auto hidden h-10 lg:flex"
          >
            <MixerHorizontalIcon className="mr-2 h-4 w-4" />
            Gem & Vis
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end" className="w-[150px]">
          <DropdownMenuLabel>Gem & Vis</DropdownMenuLabel>
          <DropdownMenuSeparator />
          {table
            .getAllColumns()
            .filter(
              (column) =>
                typeof column.accessorFn !== "undefined" && column.getCanHide()
            )
            .map((column) => {
              return (
                <DropdownMenuCheckboxItem
                  key={column.id}
                  className="capitalize"
                  checked={column.getIsVisible()}
                  onCheckedChange={(value) => column.toggleVisibility(!!value)}
                >
                  {/* @ts-ignore */}
                  {column.columnDef.meta.name}
                </DropdownMenuCheckboxItem>
              );
            })}
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
}
