import { parseNumber } from "@telerik/kendo-intl";

const imageFileRegex: RegExp = new RegExp(/\.PNG|\.JPG\.GIF|\.BMP|\.TIFF/, "i");
const zipFileRegex: RegExp = new RegExp(/\.zip/, "i");
export const emailRegex: RegExp = new RegExp(/\S+@\S+\.\S+/);

export const emailValidator = (value: string) => emailRegex.test(value) ? "" : "Please enter a valid email.";
export function isEmpty(value: any){
  return value && value.length ? "" : "This field cannot be empty"
}
export function isEmptyNoMsg(value: any){
  return value && value.length ? "" : " "
}
export const isNumberErrorMsg = (value: any) => {
  const isEmpty = value && value.length ? "" : "This field cannot be empty"
  const isNumber = /^\d+$/.test(value) ? "" : "The field can only contain an integer"
  return isEmpty ? isEmpty : isNumber
}
export const isNumberOrDecimal = (value: any) => {
  const isEmpty = value && value.length ? "" : "This field cannot be empty"
  const isNumber = /^[0-9]+([.,]?[\d]+)?$/.test(value) ? "" : "The field can contain a number or a number with a dot"
  return isEmpty ? isEmpty : isNumber
}
export const isNumberOrDecimalStrict = (value: any) => {
  return !value? "" : /^[0-9]+([.,]?[\d]+)?$/.test(value) ? "" : "The field can contain a number or a number with a dot"
}
export const isNumberStrictErrorMsg = (value: any) => {
  return !value? "" : /^\d+$/.test(value) ? "" : "The field can only contain an integer"
}
export const isValidFormattedNumber = (value: any) => {
  const valueToCheck = value ? parseNumber(value).toString() : ""
  const isNumber = /^\d+$/.test(valueToCheck) ? "" : "The field can only contain an integer"
  return value ? isNumber : ''
}
export function isImage(value: any){
  const isImage = imageFileRegex.test(value)
  if(!value) return 'An image file is required'
  return isImage && value.length ? '' : 'Supported image formats: png, jpg, gif, bmp, tiff'
}
export function isZip(value: any){
  const isImage = zipFileRegex.test(value)
  if(!value) return 'A model file is required'
  return isImage && value.length ? '' : 'The file must be in zip format'
}