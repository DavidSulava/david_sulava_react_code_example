import { ICommonModalProps } from './common';

export interface IModalCommonProps {
  headerText: string,
  isOpen: boolean,
  className?: string,
  onClose?: () => void,
  buttons: IModalWrapperButton[],
  children: JSX.Element|string
}

export interface IModalWrapperButton {
  buttonText: string
  buttonDisabled?: boolean
  onButtonClick: () => void
}

export interface ICreateNewProductModalProps extends ICommonModalProps {
  // organizationId: string
}

