import "@/styles/globals.css";

import type { Metadata } from "next";
import { cookies as getCookies } from "next/headers";
import fonts from "@/assets/fonts";

import { ApiProvider } from "@/lib/api";
import { CookiesProvider } from "@/lib/cookies";
import { cn } from "@/lib/utils";
import { AppProvider } from "@/components/app/provider";

export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app"
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  const cookies = getCookies();

  return (
    <html lang="en" className={cn(fonts.sansFont.variable)} suppressHydrationWarning>
      <body className="bg-background text-foreground">
        <CookiesProvider value={cookies.getAll().map((cookie) => ({ name: cookie.name, value: cookie.value }))}>
          <ApiProvider>
            <AppProvider>{children}</AppProvider>
          </ApiProvider>
        </CookiesProvider>
      </body>
    </html>
  );
}
