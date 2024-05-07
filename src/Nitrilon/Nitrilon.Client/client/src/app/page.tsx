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
import NineSkeletonCardComponents from "@/components/NineSkeletonCardComponents";

// Types
import { Event } from "@/lib/types";
import { Button } from "@/components/ui/button";
import Link from "next/link";

// Variables
const API_EVENTS_URL = `${
  process.env.NEXT_PUBLIC_API_URL
}/api/Event/GetEventsAfterDate?date=${new Date().toISOString()}`;
const API_EVENT_RATING_URL = `${process.env.NEXT_PUBLIC_API_URL}/api/EventRating`;

/**
 * Page for event selection that then leads the user to rate the event. Rate the event will run in a loop forever.
 * @returns JSX Element (Used for an entire page)
 */
export default function Home() {
  // States
  const [events, setEvents] = useState<Event[]>([]); // Events state
  const [showModal, setShowModal] = useState(false); // Modal state
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null); // Selected event
  const [error, setError] = useState(false); // Error state for showing error alert box

  // Functions

  /**
   * Function to handle the event click and set the selected event
   * @param event Event
   * @returns void
   */
  const handleEventClick = useCallback((event: Event) => {
    setEvents([]);
    setSelectedEvent(event);
  }, []);

  /**
   * Function to handle the grade click and send the grade to the API
   * @param grade number
   * @returns void
   */
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
    /**
     * Function to fetch the events from the API
     * @returns void
     * @sideeffect Sets the events state
     * @sideeffect Sets the progress state
     * @sideeffect Sets the error state
     * @sideeffect Shows a toast if there is an error
     */
    async function fetchEvents() {
      try {
        const response = await fetch(API_EVENTS_URL, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
        });
        if (!response.ok) {
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
        setEvents(data);
      } catch (error) {
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

  // Variables

  // isLoading is true if there are no events, no selected event and no modal is shown
  const isLoading = events.length === 0 && !selectedEvent && !showModal;

  /**
   * JSX Element
   * @returns JSX.Element
   * @description The main page for the event rating
   * @description If there are no events, it will show a message
   * @description If there is an error, it will show an error message
   */
  return (
    <main className="flex flex-wrap overflow-auto h-screen min-h-screen justify-between gap-5 p-5">
      {/**
       * Shows if there is no selected event.
       *
       * This contains a button to go to the member overview and the event overview.
       * Also mode toggle is shown here.
       */}
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
      {/**
       * All events are shown here.
       * Events can be clicked, which will remove all the events, set the selectedEvent and make the user able to rate that event.
       * */}
      {!isLoading &&
        events.map((event) => (
          // Card is a component from ShadCN Component library.
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
              onClick={() => handleGradeClick(1)}
            />
          </div>
          <div style={{ width: "100%", height: "100%", position: "relative" }}>
            <Image
              src="/images/emoji/neutral.png"
              alt="Neutral emoji"
              layout="fill"
              objectFit="contain"
              onClick={() => handleGradeClick(2)}
            />
          </div>
          <div style={{ width: "100%", height: "100%", position: "relative" }}>
            <Image
              src="/images/emoji/angry.png"
              alt="Angry emoji"
              layout="fill"
              objectFit="contain"
              onClick={() => handleGradeClick(3)}
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
      {isLoading && !error && <NineSkeletonCardComponents />}
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
