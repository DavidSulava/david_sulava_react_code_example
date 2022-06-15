import React from 'react';
import { Button } from 'react-bootstrap';

const SecurityPage = () => {
  return(
    <>
      <h6>Security</h6>
      <div className="account-info-details account-security">
        <div>
          <Button disabled={true}>Enable 2FA</Button>
        </div>
      </div>
    </>
  )
}

export default SecurityPage