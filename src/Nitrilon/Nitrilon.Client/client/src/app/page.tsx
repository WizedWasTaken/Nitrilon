"use client";
import { useEffect, useState } from "react";

type Event = {
  EventId: string;
  name: string;
  date: string;
  description: string;
  attendees: number;
};

export default function Home() {
  const [events, setEvents] = useState<Event[]>([]);

  // Fetch data from the server
  useEffect(() => {
    fetch("https://localhost:7097/api/Event")
      .then((res) => res.json())
      .then((data) => setEvents(data));
  }, []);

  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <h1>Forside</h1>
      <p>Her kommer en liste over events</p>
      <ul>
        {events.map((event) => (
          <li key={event.EventId}>
            <h2>{event.name}</h2>
            <p>{event.date}</p>
            <p>{event.description}</p>
            <p>{event.attendees}</p>
          </li>
        ))}
      </ul>
    </main>
  );
}
