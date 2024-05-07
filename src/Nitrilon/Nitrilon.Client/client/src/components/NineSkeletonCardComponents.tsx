import { useState } from "react";
import { Skeleton } from "./ui/skeleton";
import {
  Card,
  CardTitle,
  CardHeader,
  CardDescription,
  CardContent,
  CardFooter,
} from "@/components/ui/card";

export default function NineSkeletonCardComponents() {
  const [numberOfCards, setNumberOfCards] = useState<number>(9);

  return (
    <section className="flex flex-wrap overflow-auto h-screen min-h-screen justify-between gap-5 p-5">
      {[...Array(numberOfCards)].map((_, index) => (
        // Card is a component from ShadCN Component library.
        <Card key={index} className="w-full md:w-3/12 cursor-pointer">
          <CardHeader className="gap-2">
            <Skeleton className="h-3 w-1/5" />
            <Skeleton className="h-4 w-4/5" />
          </CardHeader>
          <CardContent className="h-6" />
          <CardFooter>
            <Skeleton className="h-6 w-[120px]" />
          </CardFooter>
        </Card>
      ))}
    </section>
  );
}
