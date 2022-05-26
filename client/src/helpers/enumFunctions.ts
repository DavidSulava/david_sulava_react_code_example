import { IStdEnum } from '../types/common';

export const enumGetKeys = (enumObj:IStdEnum) => Object.values(enumObj).filter(value => typeof value === 'string')

export const enumGetKey = (value: number, enumObj:IStdEnum) => {
  return Object.entries(enumObj).filter((k,v)=> {
    return typeof k[1] === 'number' && k[1] === value
  }).map((k, v) => {
    return k[0]
  })[0]
}

export const enumToKeqValue = (enumObj: IStdEnum) => {
  return Object.entries(enumObj).filter((k,v)=> {
    return typeof k[1] === 'number'
  }).map((k, v) => {
    return {key: k[0], value: k[1]}
  })
}
