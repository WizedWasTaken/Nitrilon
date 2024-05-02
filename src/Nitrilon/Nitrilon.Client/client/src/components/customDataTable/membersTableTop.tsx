import { Button } from "@/components/ui/button";

export default function MembersTableTop() {
  return (
    <div className="flex mb-5 justify-end items-center">
      <Button onClick={() => alert("This no workey workey")}>
        Tilføj medlem
      </Button>
    </div>
  );
}
