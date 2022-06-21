import { Loader } from '@progress/kendo-react-indicators';
import React, { FC } from 'react';

interface IGridLoaderProps {
  isLoading: boolean,
  text?: string
}
const GridLoader: FC<IGridLoaderProps> = ({
  isLoading,
  text= 'There is no data available'
}) => {
  return(
    <>
      {
        isLoading? <Loader type="converging-spinner" /> : text
      }
    </>
  )
}

export default GridLoader