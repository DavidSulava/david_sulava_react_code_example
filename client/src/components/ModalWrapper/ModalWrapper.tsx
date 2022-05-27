import React from 'react';
import { Button } from "react-bootstrap";
import { IModalCommonProps } from '../../types/Modal';
import { DialogActionsBar } from '@progress/kendo-react-dialogs/dist/es/DialogActionsBar';
import { Dialog } from '@progress/kendo-react-dialogs/dist/es/Dialog';

const ModalWrapper: React.FC<IModalCommonProps> = ({
  headerText,
  isOpen = false,
  className,
  onClose,
  buttons,
  children
}) => {
  return (
    <>
      {
        isOpen &&
        <Dialog title={headerText} closeIcon={false} className={className}>
          {children}
          <DialogActionsBar layout={'end'}>
            {buttons.map((b, index) => {
              return (
                <Button variant="primary" className='button' disabled={b.buttonDisabled} onClick={b.onButtonClick} key={index}>
                  {b.buttonText}
                </Button>
              )
            })}
          </DialogActionsBar>
        </Dialog>
      }
    </>
  );
}

export default ModalWrapper