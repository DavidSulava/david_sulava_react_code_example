import { parseNumber } from "@telerik/kendo-intl";
import { getter } from '@progress/kendo-data-query';

const imageFileRegex: RegExp = new RegExp(/\.PNG|\.JPG|\.GIF|\.BMP|\.TIFF/, "i");
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
export function isImage(value: File[]){
  if(!value?.length) return 'An image file is required'
  const isImage = value.every(el => imageFileRegex.test(el.name))
  return isImage && value.length ? '' : 'Supported image formats: png, jpg, gif, bmp, tiff'
}
export function isZip(value: File[]){
  if(!value?.length) return 'Please select a file'
  const isImage = value.every(el => zipFileRegex.test(el.name))
  return isImage && value.length ? '' : 'The file must be in zip format'
}

export const passwordValidation = (values: any, pwdField = 'password', confirmPwdField = 'confirmPassword') => {
  const password: string = getter(pwdField)(values);
  const passConfirm: string = getter(confirmPwdField)(values);

  const passRegex: RegExp = new RegExp(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])/);

  let msg = ""
  if(password !== passConfirm)
    msg = 'passwords do not match'
  else if(!password?.length || password?.length < 8 || passConfirm?.length < 8)
    msg = 'password must contain at least 8 characters'
  else if(!passRegex.test(password))
    msg = 'The password must contain at least one : uppercase letter, lowercase letter, number.'

  return {
    ["password"]: msg ? ' ' : '',
    ["confirmPassword"]: msg,
  };
};