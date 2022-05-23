export interface IModalCommonProps{
  headerText: string,
  isOpen: boolean,
  className?: string,
  onClose?: () => void,
  buttons: IModalWrapperButton[],
  children: JSX.Element | string
}
export interface IModalWrapperButton {
  buttonText: string
  buttonDisabled?: boolean
  onButtonClick: () => void
}

export interface ICreateNewOrganisationModalProps {
  isOpen: boolean
  onSubmit?: <T>(data?:T) => void,
  onClose: () => void,
}

export interface ICreateNewProductModalProps extends ICreateNewOrganisationModalProps{
  organizationId: string
}