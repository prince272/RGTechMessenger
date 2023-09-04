"use client";

import { FC, useCallback, useEffect, useState } from "react";
import NextLink from "next/link";
import { usePathname } from "next/navigation";
import { AddIcon, DockPanelRightIcon } from "@/assets/icons";
import { Button } from "@nextui-org/react";
import queryString from "query-string";

import { useApi, User, useUser } from "@/lib/api";
import { useSignalREffect } from "@/lib/signalr";

export interface AppNavigationProps {}

export const AppNavigation: FC<AppNavigationProps> = () => {
  const pathname = usePathname();
  const api = useApi();
  const currentUser = useUser();
  const [onlineUsers, setOnlineUsers] = useState<{ action: "idle" | "loading"; error?: any; data?: User[] }>({ action: "loading" });

  const loadOnlineUsers = useCallback(async () => {
    try {
      const response = await api.get("/users", { params: { online: true } });
      setOnlineUsers({ action: "idle", data: (response.data as any).items });
    } catch (error) {
      setOnlineUsers({ action: "idle", error });
    }
  }, [api]);

  useEffect(() => {
    loadOnlineUsers();
  }, []);

  useSignalREffect(
    "UserConnected",
    () => {
      loadOnlineUsers();
      console.log("UserConnected");
    },
    []
  );

  useSignalREffect(
    "UserDisconnected",
    () => {
      loadOnlineUsers();
      console.log("UserDisconnected");
    },
    []
  );

  return (
    <main className=" flex h-full w-[270px] flex-col  justify-between bg-content2 px-2.5 py-2.5 text-left text-content2-foreground">
      {/* new chat section */}
      <section className=" flex w-full  flex-row justify-between hover:cursor-pointer">
        <span className=" flex h-12 w-full items-center rounded-lg border-[1px] border-divider px-3 text-[14px] hover:bg-content3">
          <AddIcon className="h-5 w-5" />
          <p className=" ml-3">New chat</p>
        </span>

        {close && (
          <span onClick={close} className="ml-2.5 flex h-12 w-14 items-center justify-center rounded-lg border-[1px] border-divider hover:bg-content3">
            <DockPanelRightIcon className="h-5 w-5" />
          </span>
        )}
      </section>
      {/* user section */}
      <section className=" flex flex-col border-t-[1px] border-divider py-6">
        <div className="mb-3">Online Users</div>
        {onlineUsers.data ? (
          <>
            {onlineUsers.data.map((user) => {
              return (
                <div key={user.id} className="my-3 flex items-center">
                  <div className=" mr-2 flex h-10 w-10 items-center justify-center rounded bg-primary-500">
                    <p className=" text-2xl ">{user?.firstName.slice(0, 1)}</p>
                  </div>
                  <p>
                    {user.firstName} {user.lastName}
                  </p>
                </div>
              );
            })}
          </>
        ) : (
          <></>
        )}
      </section>
      {/* user section */}
      <section className=" flex items-center border-t-[1px] border-divider py-6">
        {!currentUser && (
          <>
            <Button as={NextLink} className="mb-4" fullWidth color="primary" href={queryString.stringifyUrl({ url: pathname, query: { dialogId: "sign-in" } })}>
              Sign in
            </Button>
          </>
        )}
      </section>
    </main>
  );
};
