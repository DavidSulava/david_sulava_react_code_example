import React from "react"

interface ISpinnerProps {
  style?: {}
  className?: string
}
const Spinner: React.FC<ISpinnerProps> = ({style, className}) => {
  return(

   <div className={`spinner-wrapper ${className}`} style={style}>
     <svg className="spinner" viewBox="0 0 50 50">
       <circle className="path" cx="25" cy="25" r="20" fill="none" strokeWidth="5"></circle>
     </svg>
   </div>
  )
}

export default Spinner