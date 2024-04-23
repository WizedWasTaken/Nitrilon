"use client";

import { useEffect, useState } from "react";

// Types
import { Event } from "@/lib/types";

// Variables
const API_EVENTS_URL = `https://localhost:7097/api/Event`;
const API_EVENT_RATING_URL = "https://localhost:7097/api/EventRating";

export default function AdminPage() {
  const [events, setEvents] = useState<Event[]>([]);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);

  useEffect(() => {
    async function fetchEvents() {
      const response = await fetch(API_EVENTS_URL, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (!response.ok) {
        alert("Failed to fetch events");
        return;
      }

      const data = await response.json();
      setEvents(data);
    }

    fetchEvents();
  }, []);

  return (
    <div className="flex">
      <aside className="w-1/4 h-full">
        {events.map((event) => (
          <div
            key={event.id}
            className="collapse collapse-arrow bg-base-200"
            onClick={() => setSelectedEvent(event)}
          >
            <input type="radio" name="my-accordion-2" />
            <div className="collapse-title text-xl font-medium">
              <b>{event.name}</b> {new Date(event.date).toLocaleDateString()}
            </div>
            <div className="collapse-content">
              <p>
                <b>{event.description}</b>
              </p>
              <p>
                <b>Deltagere:</b>{" "}
                {event.attendees == 0 || event.attendees == -1
                  ? "Ingen"
                  : `${event.attendees}`}{" "}
                deltagere
              </p>
            </div>
          </div>
        ))}
      </aside>
      <h1>{selectedEvent?.name}</h1>
    </div>
  );
}
