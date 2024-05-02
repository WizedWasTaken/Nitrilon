import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Member } from "@/lib/types";
import { toast } from "sonner";

export default function MembersTableTop({
  members,
  createNewMember,
}: {
  members: Member[];
  createNewMember: (
    name: string,
    phoneNumber: string,
    email: string,
    enrollmentDate: Date,
    membershipId: number
  ) => void;
}) {
  const [name, phoneNumber, email, enrollmentDate] = [0, 1, 2, 3];

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
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
    const enrollmentDate = formData.get("enrollmentDate") as string;
    const membershipId = 1;

    if (!name || !phoneNumber || !email || !enrollmentDate) {
      toast.error("Udfyld alle felter", {
        duration: 5000,
      });

      return;
    }
    createNewMember(
      name,
      phoneNumber,
      email,
      new Date(enrollmentDate),
      membershipId
    );
  }
  return (
    <div className="flex mb-5 justify-end items-center">
      <Dialog>
        <DialogTrigger asChild>
          <Button>Opret nyt medlem</Button>
        </DialogTrigger>
        <DialogContent>
          <DialogTitle>Oprettelse af nyt medlem</DialogTitle>
          <DialogDescription>
            Udfyld felterne for at oprette et nyt medlem.
          </DialogDescription>
          <form onSubmit={handleSubmit}>
            <Label htmlFor="name">Navn</Label>
            <Input name="name" id="name" type="text" required />
            <Label htmlFor="phoneNumber">Telefonnummer</Label>
            <Input name="phoneNumber" id="phoneNumber" type="text" required />
            <Label htmlFor="email">Email</Label>
            <Input name="email" id="email" type="email" required />
            <Label htmlFor="enrollmentDate">Indmeldelsesdato</Label>
            <Input
              name="enrollmentDate"
              id="enrollmentDate"
              type="date"
              defaultValue={new Date().toISOString().split("T")[0]}
              required
            />
            <DialogFooter>
              <DialogClose asChild>
                <Button variant="secondary">Annuller</Button>
              </DialogClose>
              <DialogClose asChild>
                <Button type="submit">Opret</Button>
              </DialogClose>
            </DialogFooter>
          </form>
        </DialogContent>
      </Dialog>
    </div>
  );
}
