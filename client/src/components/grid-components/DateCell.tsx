import { GridCellProps } from '@progress/kendo-react-grid';
import React from 'react';

export const DateCell = (props: GridCellProps) => {
  const field = props.field || "";
  const dateString = new Date(props.dataItem[field]).toLocaleDateString();
  return (
    <td>{dateString}</td>
  )
}