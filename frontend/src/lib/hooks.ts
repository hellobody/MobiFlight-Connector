import { AppMessageKey, DeviceElementType } from "@/types"
import { AppMessage, FrontendMessageType } from "@/types/messages"
import { useEffect } from "react"
import { IDictionary } from "@/types/config"

export const publishOnMessageExchange = () => ({
  publish: (message: FrontendMessageType) => {
    console.log(`Publishing FrontendMessage -> ${message.key}`)
    window.chrome?.webview?.postMessage(message)
  },
})
// create a useAppMessage hook that listens for messages
// the paramaters are the AppMessageKey and the onReceiveMessage callback
// the callback is called when a message is received
export const useAppMessage = (
  key: AppMessageKey,
  onReceiveMessage: (message: AppMessage) => void
) => {
  useEffect(() => {
    const onReveiveMessageHandler = (event: Event) => {
      try {
        const appMessage = JSON.parse(
          (event as MessageEvent).data
        ) as AppMessage
        if (appMessage.key === key) {
          console.log(`Received AppMessage -> ${appMessage.key}`)
          onReceiveMessage(appMessage)
        }
      } catch (error) {
        console.error("Error parsing message", error)
      }
    }

    // add an event listener for the message key
    window.chrome?.webview?.addEventListener("message", onReveiveMessageHandler)
    return () => {
      // remove the event listener when the component is unmounted
      window.chrome?.webview?.removeEventListener(
        "message",
        onReveiveMessageHandler
      )
    }
  }, [key, onReceiveMessage])
}
