"use client";

import { useEffect, useState } from "react";

// Types
import { Event } from "@/lib/types";

import { Line, Doughnut } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  ChartData,
  ArcElement,
  BarElement,
} from "chart.js";

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
const API_EVENTS_URL = `https://localhost:7097/api/Event`;
const API_EVENT_RATING_URL = "https://localhost:7097/api/EventRating";

export default function AdminPage() {
  const [events, setEvents] = useState<Event[]>([]);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [chartData, setChartData] = useState({});

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

      // Count ratings
      const ratingCount = { 1: 0, 2: 0, 3: 0 };
      ratings.forEach((rating: { ratingId: 1 | 2 | 3 }) => {
        ratingCount[rating.ratingId]++;
      });

      // Update the chart data
      setChartData({
        labels: ["Glad", "Neutral", "Utilfreds"],
        datasets: [
          {
            label: "Antal af ratings",
            data: [ratingCount[1], ratingCount[2], ratingCount[3]],
            backgroundColor: [
              "rgba(255, 99, 132, 0.2)",
              "rgba(54, 162, 235, 0.2)",
              "rgba(75, 192, 192, 0.2)",
            ],
            borderColor: [
              "rgba(255, 99, 132, 1)",
              "rgba(54, 162, 235, 1)",
              "rgba(75, 192, 192, 1)",
            ],
            borderWidth: 1,
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
      <aside className="w-1/4 h-full overflow-y-scroll">
        {events.map((event) => (
          <div
            key={event.id}
            className="collapse collapse-arrow bg-base-200"
            onClick={() => handleSelectedEvent(event)}
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
      {/* Charts container */}
      <section className="w-full h-full p-5 gap-5">
        <h1 className="text-5xl font-bold text-center w-full">
          {selectedEvent?.name}
        </h1>
        {selectedEvent && (
          <div className="flex h-full gap-5 py-5">
            <div className="w-1/2 bg-gray-800 rounded-md">
              <Doughnut
                data={{
                  datasets: [],
                  ...chartData,
                }}
                options={{
                  responsive: true,
                  maintainAspectRatio: false,
                }}
              />
            </div>
            <div className="w-1/2 bg-gray-800 rounded-md">
              <Line
                data={{
                  datasets: [],
                  ...chartData,
                }}
                options={{
                  responsive: true,
                  maintainAspectRatio: false,
                }}
              />
            </div>
          </div>
        )}
      </section>
    </div>
  );
}
