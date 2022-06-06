import { useEffect, RefObject } from "react"

type CallBackProp = (ev?:MouseEvent) => void

const useClickOutside = (ref: RefObject<any>, callback: CallBackProp) => {

  useEffect(() => {
    document.addEventListener("click", handleClick);
    return () => document.removeEventListener("click", handleClick);
  }, [])

  const handleClick = (ev:MouseEvent) => {
    if (ref.current == null || !ref.current.contains(ev.target))
      callback(ev);
  }
}

export default useClickOutside