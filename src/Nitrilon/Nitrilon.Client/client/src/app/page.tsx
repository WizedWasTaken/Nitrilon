"use client";
import { useEffect, useState } from "react";
import Image from "next/image";

type Event = {
  id: string;
  name: string;
  date: string;
  description: string;
  attendees: number;
};

export default function Home() {
  const [events, setEvents] = useState<Event[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);

  function handleEventClick(event: Event) {
    console.log("Event clicked", event);
    setEvents([]);
    setSelectedEvent(event);
  }

  async function handleGradeClick(grade: number) {
    fetch(
      `https://localhost:7097/api/EventRating?EventId=${selectedEvent?.id}&RatingId=${grade}`,
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
  }

  // Fetch data from the server
  useEffect(() => {
    fetch("https://localhost:7097/api/Event")
      .then((res) => res.json())
      .then((data) => setEvents(data));
  }, []);

  return (
    <main className="flex flex-wrap h-screen justify-around items-center gap-5 p-20">
      {events.map((event) => (
        <div
          key={event.id}
          className="border p-4 w-full md:w-3/12 h-[150px] cursor-pointer bg-gray-400 text-white rounded-md relative"
          onClick={() => handleEventClick(event)}
        >
          <h2>{event.name}</h2>
          <p>{new Date(event.date).toLocaleDateString()}</p>
          <p>{event.description}</p>
          <p>{event.attendees === -1 ? "Ingen" : event.attendees} deltagere</p>
        </div>
      ))}
      {selectedEvent && !showModal && (
        <>
          <h1 className="absolute top-5 w-full text-center text-4xl font-bold">
            Hvad synes du om besøget?
          </h1>
          <button
            className="w-[30%] h-[70%] relative"
            onClick={() => handleGradeClick(3)}
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
              src="/images/emoji/neutral.jpg"
              alt="Neutral emoji"
              layout="fill"
              objectFit="contain"
            />
          </button>
          <button
            className="w-[30%] h-[70%] text-6xl relative"
            onClick={() => handleGradeClick(1)}
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
      {!events.length && !selectedEvent && !showModal && <p>Loading...</p>}
    </main>
  );
}
