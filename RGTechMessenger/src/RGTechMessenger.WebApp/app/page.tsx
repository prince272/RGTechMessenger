"use client";

import { AddIcon, DismissIcon, DockPanelRightIcon, NavigationIcon, SendIcon } from "@/assets/icons";

import { AppNavigation } from "@/components/app/navigation";
import { useApp } from "@/components/app/provider";

export default function Home() {
  const app = useApp();
  return (
    <main className=" flex h-screen justify-between">
      {/* large screen */}
      <div className=" hidden md:block">
        {app.navigation.opened && (
          <section className=" h-full">
            <AppNavigation />
          </section>
        )}
      </div>

      {/* small screen */}
      <div className="md:hidden">
        {app.navigation.opened && (
          <div className=" fixed bottom-0 left-0 right-0 top-0">
            <div className="flex h-full">
              <section className=" h-full">
                <AppNavigation />
              </section>

              <div className="flex w-full justify-start text-foreground">
                <span
                  onClick={() => app.navigation.close()}
                  className="m-3 flex h-10 w-10 items-center justify-center rounded-lg border-[1px] border-gray-200 hover:cursor-pointer hover:bg-[#3a3b3f]"
                >
                  <DismissIcon className="h-5 w-5" />
                </span>
              </div>
            </div>
          </div>
        )}
      </div>

      <section className=" flex h-full w-full flex-col justify-between">
        {/* other content */}
        <div className="">
          {/* large screen */}
          <div className="hidden md:block">
            {!app.navigation.opened && (
              <div className="m-6 hover:cursor-pointer">
                <DockPanelRightIcon color="white" onClick={() => app.navigation.open()} className="h-5 w-5" />
              </div>
            )}
          </div>

          {/* small screen */}
          <div className=" border-b-[1px] border-divider py-5 md:hidden">
            <div className=" flex justify-between px-4 text-[#D4D4D4]">
              <NavigationIcon onClick={() => app.navigation.open()} className=" h-5 w-5 hover:cursor-pointer" />

              <p>New Chat</p>

              <AddIcon className=" h-5 w-5 hover:cursor-pointer" />
            </div>
          </div>
        </div>

        {/* input */}
        <div className=" py-14">
          <div className=" flex justify-center">
            <div className=" mx-7 flex w-full justify-center md:mx-0 md:w-[85%] lg:w-[75%] xl:w-[700px] ">
              <input
                className="m-auto h-[50px] w-full rounded-l-xl bg-content2 px-6 text-lg text-white hover:outline-none focus:outline-none md:h-[60px]"
                placeholder="Send a message"
              />

              <span className="flex items-center justify-center rounded-r-xl bg-content2 px-5">
                <SendIcon className="h-5 w-5" />
              </span>
            </div>
          </div>
        </div>
      </section>
    </main>
  );
}
