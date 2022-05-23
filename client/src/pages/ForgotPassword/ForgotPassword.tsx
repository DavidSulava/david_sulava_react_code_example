import { ERoutes } from '../../router/Routes';
import React from 'react';
import BtnLink from '../../components/BtnLink';

const ForgotPassword = () => {
 return(
   <div className="sign-in-form">
     Forgot Password
     <br/>

     <br/>
     <BtnLink to={ERoutes.SignUp} className="btn btn-outline-primary mb-1">Sign Up</BtnLink>
     <BtnLink to={ERoutes.Root}className="btn btn-outline-primary mb-1" >Sign in</BtnLink>
   </div>
 )
}

export default ForgotPassword