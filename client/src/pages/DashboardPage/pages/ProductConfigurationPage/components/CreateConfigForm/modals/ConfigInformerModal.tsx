import React, { FC } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { IModalWrapperButton } from '../../../../../../../types/modal';
import setPath from '../../../../../../../helpers/setPath';
import { ERoutes } from '../../../../../../../router/Routes';
import ModalWrapper from '../../../../../../../components/ModalWrapper/ModalWrapper';
import { ICommonModalProps } from '../../../../../../../types/common';

interface IConfigCreatedProps extends ICommonModalProps {
  isDataCashed?: boolean,
  cachedConfId?: string
}

export const ConfigInformerModal: FC<IConfigCreatedProps> = ({
  isOpen,
  onClose,
  cachedConfId,
  isDataCashed = false
}) => {
  const navigate = useNavigate();
  const {organizationId, productId, versionId} = useParams();

  const textBody = isDataCashed ? 'Configuration already exists in cache.' : 'Configuration request created, please wait for it to be processed.'
  const submitBtnText = isDataCashed ? 'View configuration' : 'View list'
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: 'close', onButtonClick: () => onClose()},
    {buttonText: submitBtnText, onButtonClick: () => onSubmitInner()}
  ]

  const onSubmitInner = () => {
    if(!isDataCashed)
      return navigate(setPath(ERoutes.ProdVersion, [organizationId, productId, versionId]))
    else if(isDataCashed && cachedConfId) {
      navigate(setPath(ERoutes.ProdVersionConfig, [organizationId, productId, versionId, cachedConfId]))
    }
    onClose()
  }

  return (
    <ModalWrapper
      headerText={''}
      isOpen={isOpen}
      onClose={onClose}
      buttons={modalButtons}
    >
      {textBody}
    </ModalWrapper>
  )
}