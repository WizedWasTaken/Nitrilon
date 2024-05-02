import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.scss";
import { Toaster } from "@/components/ui/sonner";
import { ThemeProvider } from "next-themes";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Nitrilon - Noah A. Nielsen",
  description: "S2 - Nitrilon - Noah A. Nielsen",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className="min-h-screen dark">
      <link rel="icon" href="/logo.ico" sizes="any" />

      <body className={`${inter.className} min-h-screen overflow-x-hidden`}>
        <ThemeProvider
          attribute="class"
          defaultTheme="system"
          enableSystem
          disableTransitionOnChange
        >
          <main>{children}</main>
          <Toaster
            toastOptions={{
              classNames: {
                error: "bg-red-500 text-white",
                success: "bg-green-500 text-white",
                info: "bg-blue-500 text-white",
                warning: "bg-yellow-500 text-white",
              },
            }}
          />
        </ThemeProvider>
      </body>
    </html>
  );
}
