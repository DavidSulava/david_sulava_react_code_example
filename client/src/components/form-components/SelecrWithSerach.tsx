import {
  DropDownList,
  DropDownListChangeEvent,
  DropDownListFilterChangeEvent,
  DropDownListPageChangeEvent
} from '@progress/kendo-react-dropdowns';
import React, { FC, useCallback, useEffect, useRef, useState } from 'react';
import { ICommonObject } from '../../types/common';
import { FieldRenderProps, FieldWrapper } from '@progress/kendo-react-form';

interface ISelectWithSearchProps extends FieldRenderProps {
  textField: string,
  keyField: string,
  selectLength?: number,
  loading?: boolean,
  data: any,
  onRequestData: (skip: number, take: number, filter: string) => void,
}

const CSelectWithSearch: FC<ISelectWithSearchProps> = ({
  label,
  data,
  name,
  className = '',
  labelClassName,
  loading = false,
  valid,
  textField,
  keyField,
  selectLength,
  onRequestData,
  onChange,
}) => {
  const errorClassName = valid ? "" : "input-error k-invalid"

  const [dataInner, setDataInner] = useState<any[]>([]);
  const [valueInner, setValueInner] = useState<any>(null);
  const [filter, setFilter] = useState("");
  const selectLengthInner = selectLength ?? dataInner.length

  useEffect(() => {
    if(!filter){
      setValueInner(data[0])
      onChange({value: data[0]})
    }
    setDataInner(data)
  }, [data])
  useEffect(() => {
    // onRequestData(0, selectLengthInner, filter);
  }, [filter]);

  const onFilterChange = useCallback((event: DropDownListFilterChangeEvent) => {
    const filter = event.filter.value;
    onRequestData(0, selectLengthInner, filter);
    setFilter(filter);
  }, []);

  const onChangeInner = useCallback((event: DropDownListChangeEvent) => {
    const value = event.target.value;
    setValueInner(value);
    onChange(event)
  }, []);

  return (
    <FieldWrapper>
      {label && (
        <label className={`${labelClassName ?? ""} ${errorClassName}`}>{label}</label>
      )}
      <br/>
      <DropDownList
        className={`${className} ${errorClassName}`}
        name={name}
        data={dataInner}
        value={valueInner}
        dataItemKey={keyField}
        textField={textField}
        filterable={true}
        onChange={onChangeInner}
        onFilterChange={onFilterChange}
        loading={loading}
      />
    </FieldWrapper>
  );
}

export default CSelectWithSearch