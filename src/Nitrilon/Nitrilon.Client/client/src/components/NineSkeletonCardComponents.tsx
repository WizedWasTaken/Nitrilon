import { useState } from "react";
import { Skeleton } from "./ui/skeleton";
import {
  Card,
  CardHeader,
  CardContent,
  CardFooter,
} from "@/components/ui/card";

/**
 * Function to return 9 card skeletons.
 * @returns JSX.Element
 */
export default function NineSkeletonCardComponents() {
  const [numberOfCards] = useState<number>(9);

  return (
    <section className="flex flex-wrap overflow-auto h-screen min-h-screen justify-between gap-5 p-5">
      {[...Array(numberOfCards)].map((_, index) => (
        // Card is a component from ShadCN Component library.
        <Card key={index} className="w-full md:w-3/12 cursor-pointer">
          <CardHeader>
            <Skeleton className="h-4 w-1/5" />
            <Skeleton className="h-4 w-4/5" />
          </CardHeader>
          <CardContent className="h-2" />
          <CardFooter>
            <Skeleton className="h-2 w-[160px]" />
          </CardFooter>
        </Card>
      ))}
    </section>
  );
}
