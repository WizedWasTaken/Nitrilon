"use client";

// Dependencies
import { useEffect, useState, useCallback, useMemo } from "react";
import Image from "next/image";

// Components
import {
  Card,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Progress } from "@/components/ui/progress";
import { toast } from "sonner";
import React from "react";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { ModeToggle } from "@/components/ui/dark-mode-toggle";

// Types
type Event = {
  id: string;
  name: string;
  date: string;
  description: string;
  attendees: number;
};

// Variables
const API_EVENTS_URL = `https://localhost:7097/api/Event/GetEventsAfterDate?date=${new Date().toISOString()}`;
const API_EVENT_RATING_URL = "https://localhost:7097/api/EventRating";

export default function Home() {
  const [events, setEvents] = useState<Event[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [progress, setProgress] = useState(0);
  const [error, setError] = useState(false);

  const handleEventClick = useCallback((event: Event) => {
    console.log("Event clicked", event);
    setEvents([]);
    setSelectedEvent(event);
  }, []);

  const handleGradeClick = useCallback(
    async (grade: number) => {
      await fetch(
        `${API_EVENT_RATING_URL}?EventId=${selectedEvent?.id}&RatingId=${grade}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      setShowModal(true);
      setTimeout(() => {
        setShowModal(false);
      }, 3000);
    },
    [selectedEvent]
  );

  useEffect(() => {
    async function fetchEvents() {
      setProgress(41);
      const response = await fetch(API_EVENTS_URL, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (!response.ok) {
        setProgress(100);
        toast.error("Der skete en teknisk fejl. Prøv igen senere", {
          description: `${new Date().toLocaleString()}`,
          action: {
            label: "Prøv igen",
            onClick: () => fetchEvents(),
          },
        });
        return setError(true);
      }
      const data = await response.json();
      setProgress(90);
      setTimeout(() => {
        setProgress(100);
      }, 1000);
      setEvents(data);
    }

    fetchEvents();
  }, []);

  const isLoading =
    (events.length === 0 && !selectedEvent && !showModal) || progress < 100;

  return (
    <main className="flex flex-wrap h-screen justify-around items-center gap-5 p-20">
      <div className="absolute top-5 w-full flex justify-end px-5">
        <ModeToggle />
      </div>
      {progress == 100 &&
        events.map((event) => (
          <>
            <Card
              key={event.id}
              onClick={() => handleEventClick(event)}
              className="w-full md:w-3/12 cursor-pointer"
            >
              <CardHeader>
                <CardTitle>{event.name}</CardTitle>
                <CardDescription>
                  {new Date(event.date).toLocaleDateString("da-DK")}
                </CardDescription>
                <CardDescription>
                  kl.{" "}
                  {new Date(event.date).toLocaleTimeString("da-DK", {
                    timeStyle: "short",
                  })}
                </CardDescription>
                <CardDescription>{event.description}</CardDescription>
                <CardDescription>
                  {event.attendees === -1 ? "Ingen" : event.attendees} deltagere
                </CardDescription>
              </CardHeader>
            </Card>
          </>
        ))}
      {selectedEvent && !showModal && !isLoading && (
        <>
          <h1 className="absolute text-black text-6xl text-bold dark:text-white top-10 w-full text-center">
            Giv din holdning til {selectedEvent?.name}
          </h1>
          <button
            className="w-[30%] h-[70%] relative"
            onClick={() => handleGradeClick(1)}
          >
            <Image
              src="/images/emoji/happy.webp"
              alt="Happy emoji"
              layout="fill"
              objectFit="contain"
            />
          </button>
          <button
            className="w-[30%] h-[70%] text-6xl relative"
            onClick={() => handleGradeClick(2)}
          >
            <Image
              src="/images/emoji/neutral-removebg-preview.png"
              alt="Emo emoji"
              layout="fill"
              objectFit="contain"
            />
          </button>
          <button
            className="w-[30%] h-[70%] text-6xl relative"
            onClick={() => handleGradeClick(3)}
          >
            <Image
              src="/images/emoji/sad.png"
              alt="Sad emoji"
              layout="fill"
              objectFit="contain"
            />
          </button>
        </>
      )}
      {showModal && (
        <section>
          <h1 className="text-5xl">Tak for din anmeldelse!</h1>
          <p className="text-3xl text-center">Vi ses snart igen :D</p>
        </section>
      )}
      {!isLoading && events.length === 0 && error && (
        <Alert>
          <AlertTitle>Ingen events</AlertTitle>
          <AlertDescription>
            Der er ingen events at vise. Prøv igen senere
          </AlertDescription>
        </Alert>
      )}
      {isLoading && <Progress value={progress} className="w-[60%]" />}
      {error && (
        <Alert>
          <AlertTitle>Der skete en teknisk fejl</AlertTitle>
          <AlertDescription>
            Der skete en teknisk fejl. Prøv igen senere
          </AlertDescription>
        </Alert>
      )}
    </main>
  );
}