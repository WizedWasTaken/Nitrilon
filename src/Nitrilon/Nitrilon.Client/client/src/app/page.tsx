"use client";

// Dependencies
import { useEffect, useState, useCallback } from "react";
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
import { Event } from "@/lib/types";
import { Button } from "@/components/ui/button";
import Link from "next/link";

// Variables
const API_EVENTS_URL = `${
  process.env.NEXT_PUBLIC_API_URL
}/api/Event/GetEventsAfterDate?date=${new Date().toISOString()}`;
const API_EVENT_RATING_URL = `${process.env.NEXT_PUBLIC_API_URL}/api/EventRating`;

export default function Home() {
  const [events, setEvents] = useState<Event[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [progress, setProgress] = useState(0);
  const [error, setError] = useState(false);

  const handleEventClick = useCallback((event: Event) => {
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
      try {
        const response = await fetch(API_EVENTS_URL, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        });
        setProgress(84);
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
        setProgress(100);
        setEvents(data);
      } catch (error) {
        setProgress(100);
        toast.error("Der skete en teknisk fejl. Prøv igen senere", {
          description: `${new Date().toLocaleString()}`,
          action: {
            label: "Prøv igen",
            onClick: () => fetchEvents(),
          },
        });
        setError(true);
      }
    }

    fetchEvents();
  }, []);

  const isLoading =
    (events.length === 0 && !selectedEvent && !showModal) || progress < 100;

  return (
    <main className="flex flex-wrap overflow-auto h-screen min-h-screen justify-between gap-5 p-5">
      {!selectedEvent && (
        <article className="w-full relative flex justify-between top-0">
          <div className="flex gap-5 z-50">
            <Button asChild>
              <Link href="/overview">Medlem overblik</Link>
            </Button>
            <Button asChild>
              <Link href="/admin">Event overblik</Link>
            </Button>
          </div>
          <h1 className="text-4xl font-bold absolute text-center w-full">
            Vælg et event
          </h1>
          {!selectedEvent && !showModal && <ModeToggle />}
        </article>
      )}
      {!isLoading &&
        events.map((event) => (
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
                {event.attendees === -1 || event.attendees === 0
                  ? "Ingen"
                  : event.attendees}{" "}
                deltagere
              </CardDescription>
            </CardHeader>
          </Card>
        ))}
      {selectedEvent && !showModal && !isLoading && (
        <div className="h-full w-full flex items-center justify-center space-x-10">
          <h1 className="absolute text-black text-5xl font-bold dark:text-white top-5 w-full text-center">
            Giv din holdning til {selectedEvent.name}
          </h1>
          <div style={{ width: "100%", height: "100%", position: "relative" }}>
            <Image
              src="/images/emoji/smile.png"
              alt="Happy emoji"
              layout="fill"
              objectFit="contain"
              unoptimized
            />
          </div>
          <div style={{ width: "100%", height: "100%", position: "relative" }}>
            <Image
              src="/images/emoji/neutral.png"
              alt="Neutral emoji"
              layout="fill"
              objectFit="contain"
            />
          </div>
          <div style={{ width: "100%", height: "100%", position: "relative" }}>
            <Image
              src="/images/emoji/angry.png"
              alt="Angry emoji"
              layout="fill"
              objectFit="contain"
            />
          </div>
        </div>
      )}

      {showModal && (
        <section className="absolute top-1/2 w-full text-center">
          <h1 className="text-5xl">Tak for din anmeldelse!</h1>
          <p className="text-3xl text-center">Vi ses snart igen :D</p>
        </section>
      )}
      {!isLoading && events.length === 0 && !selectedEvent && (
        <section className="container mx-auto">
          <Alert>
            <AlertTitle>Ingen events</AlertTitle>
            <AlertDescription>
              Der er ingen events at vise. Prøv igen senere
            </AlertDescription>
          </Alert>
        </section>
      )}
      {isLoading && !error && (
        <section className="absolute w-full top-[50%] left-[10%]">
          <Progress value={progress} className="w-[80%]" />
        </section>
      )}
      {error && (
        <section className="container mx-auto">
          <Alert>
            <AlertTitle>Fejl</AlertTitle>
            <AlertDescription>
              Der skete en teknisk fejl. Prøv igen senere
            </AlertDescription>
          </Alert>
        </section>
      )}
    </main>
  );
}
