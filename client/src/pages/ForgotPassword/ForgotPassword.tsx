import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import React from 'react';

const ForgotPassword = () => {
 return(
   <div className="sign-in-form">
     Forgot Password
     <br/>

     <br/>
     <Button variant="outlined" className="btn-outline-info mb-1" >
       <Link to={ERoutes.SignUp}>Sign Up</Link>
     </Button>
     <Button variant="outlined" className="btn-outline-info mb-1">
       <Link to={ERoutes.Root}>Sign in</Link>
     </Button>
   </div>
 )
}

export default ForgotPassword