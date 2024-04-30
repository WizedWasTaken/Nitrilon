export type Event = {
    id: string;
    name: string;
    date: string;
    description: string;
    attendees: number;
  };

  export type Membership = {
    membershipId: number;
    name: string;
    description: string;
  }


  export type Member = {
    memberId: number;
    name: string;
    phoneNumber: string;
    email: string;
    enrollmentDate: Date;
    membership: Membership;
  }