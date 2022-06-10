import useAuthCheck from '../../../../helpers/hooks/useAuthCheck';
import React, { useState } from 'react';
import { Button } from 'react-bootstrap';
import InfoEditModal from './Modals/infoEditModal';

const InfoDetailsPage = () => {
  const {account} = useAuthCheck()
  const [isShowInfoEdit, setIsShowInfoEdit] = useState(false)

  const onToggleInfoEditClose = () => setIsShowInfoEdit(!isShowInfoEdit)

  return (
    <>
      <h6>Personal details</h6>
      <div className="account-info-details">
        <div>
          <p><b>Email: </b> {account ? account.email : ''}</p>
          <div className="details-button-container">
            <Button disabled={true}>Edit email</Button>
          </div>
          <br/> <br/>
          <p><b>First name: </b>{account ? account.firstName : ''} </p>
          <p><b>Last name: </b> {account ? account.lastName : ''}</p>
          <p><b>Phone: </b> {account ? account.phone : ''}</p>
          <div className="details-button-container">
            <Button onClick={onToggleInfoEditClose}>Edit details</Button>
          </div>
        </div>
      </div>
      <InfoEditModal isOpen={isShowInfoEdit} onClose={onToggleInfoEditClose}/>
    </>
  )
}

export default InfoDetailsPage