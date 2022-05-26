import React, { FC } from 'react';
interface INoRecordProps {
  isLoading: boolean
}
const NoRecords: FC<INoRecordProps> = ({isLoading}) => {
  return(
    <>
      {isLoading? <span>Loading...</span> : <span> No records </span>}
    </>
  )
}
export default NoRecords