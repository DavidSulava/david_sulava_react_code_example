
const getEnumKeys = (enumObj:{[key: string|number]:string|number}) => Object.values(enumObj).filter(value => typeof value === 'string')

export default getEnumKeys