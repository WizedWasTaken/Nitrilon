"use client";

import { useEffect, useState } from "react";

// Types
import { Event } from "@/lib/types";

import { Bar, Doughnut } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  ArcElement,
  BarElement,
} from "chart.js";
import { ModeToggle } from "@/components/ui/dark-mode-toggle";

import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "Nitrilon - Admin",
  description: "S2 - Nitrilon - Noah A. Nielsen",
};

// Register the necessary components for a line chart
ChartJS.register(
  CategoryScale,
  ArcElement,
  LinearScale,
  PointElement,
  LineElement,
  BarElement,
  Title,
  Tooltip,
  Legend
);
// Variables
// Variables
const API_EVENTS_URL = `${process.env.NEXT_PUBLIC_API_URL}/api/Event`;
const API_EVENT_RATING_URL = `${process.env.NEXT_PUBLIC_API_URL}/api/EventRating`;

export default function AdminPage() {
  const [events, setEvents] = useState<Event[]>([]);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [ratingSum, setRatingSum] = useState(0);
  const [chartData, setChartData] = useState({});

  useEffect(() => {
    async function fetchEvents() {
      console.log("Fetching events");
      const response = await fetch(API_EVENTS_URL, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });
      if (!response.ok) {
        console.log(response.status, response.statusText);
        alert("Failed to fetch events");
        return;
      }

      console.log(response);
      const data = await response.json();
      setEvents(data);
    }

    fetchEvents();
  }, []);

  async function handleSelectedEvent(event: Event) {
    try {
      const response = await fetch(`${API_EVENT_RATING_URL}/${event.id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch event ratings");
      }

      const ratings = await response.json();

      // Get the number of each rating from response.
      // It will be returned as:
      // badRatingCount: 6,
      // goodRatingCount: 5,
      // neutralRatingCount: 6
      // Directly use the properties from the ratings object
      const ratingCount = {
        1: ratings.goodRatingCount,
        2: ratings.neutralRatingCount,
        3: ratings.badRatingCount,
      };

      setRatingSum(ratingCount[1] + ratingCount[2] + ratingCount[3]);

      // Update the chart data
      setChartData({
        labels: ["Glad", "Neutral", "Utilfreds"],
        datasets: [
          {
            label: "Antal af ratings",
            data: [ratingCount[1], ratingCount[2], ratingCount[3]],
            backgroundColor: [
              "rgba(34, 203, 107, 0.35)",
              "rgba(189, 200, 28, 0.35)",
              "rgba(210, 65, 21, 0.35)",
            ],
            borderColor: [
              "rgba(34, 203, 107, 0.35)",
              "rgba(189, 200, 28, 0.35)",
              "rgba(210, 65, 21, 0.35)",
            ],
            borderWidth: 4,
          },
        ],
      });

      // Use a functional update to ensure we're working with the most current state
      setSelectedEvent((currentEvent) => ({
        ...currentEvent,
        ...event,
        ratings,
      }));
    } catch (error) {
      //@ts-ignore
      alert(error.message);
    }
  }

  useEffect(() => {
    console.log(selectedEvent);
  }, [selectedEvent]);

  return (
    <div className="flex p-5 min-h-screen">
      <div className="absolute w-full flex justify-end px-7">
        <ModeToggle />
      </div>
      <aside className="h-screen join join-vertical hidden xl:block min-w-96 pb-10 overflow-y-scroll pr-7">
        {events.map((event) => (
          <div
            key={event.id}
            className="join-item collapse collapse-arrow bg-gray-300 text-black dark:bg-gray-800 dark:text-white"
            onClick={() => handleSelectedEvent(event)}
          >
            <input type="radio" name="event-accordion" />
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
      {/* Charts container */}
      <section className="w-full xl:flex hidden flex-col min-h-full max-h-screen gap-5 py-5">
        <h1 className="text-4xl font-bold text-center w-full">
          {selectedEvent?.name || "Vælg et event"}{" "}
          <span>{ratingSum} anmeldelser</span>
        </h1>
        <div className="flex-grow flex flex-col gap-3 p-5">
          <div className="flex-grow bg-gray-300 dark:bg-gray-800 rounded-md p-3">
            <Doughnut
              data={{
                datasets: [],
                ...chartData,
              }}
              options={{
                maintainAspectRatio: false,
              }}
            />
          </div>
          <div className="flex-grow bg-gray-300 dark:bg-gray-800 rounded-md p-3">
            <Bar
              data={{
                datasets: [],
                ...chartData,
              }}
              options={{
                maintainAspectRatio: false,
              }}
            />
          </div>
        </div>
      </section>
      <div className="w-full h-full xl:hidden">
        <h1 className="text-6xl">Åben denne side på en større skærm!</h1>
      </div>
    </div>
  );
}
