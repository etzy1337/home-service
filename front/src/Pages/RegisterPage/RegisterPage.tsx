import React from 'react'
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from "yup";
import { useAuth } from '../../Context/useAuth';
import { useForm } from 'react-hook-form';

type Props = {}


type RegisterFormInput={
    email:string;
    password:string;
};

const validation = Yup.object().shape({
    email: Yup.string().required("Username is required"),
    password: Yup.string().required("Password is required"),
});



const RegisterPage = (props: Props) => {
    const {registerUser} = useAuth();
    const { register, handleSubmit, formState: { errors } } = useForm<RegisterFormInput>({
            resolver: yupResolver(validation)
        });

        const handleRegister = (form:RegisterFormInput)=>{
            registerUser(form.email,form.password);
        };

    return (
        <div className="register-page">
          <h2>Register</h2>
          <form onSubmit={handleSubmit(handleRegister)}>
            <div className="form-group">
              <label htmlFor="email">Email</label>
              <input
                type="email"
                id="email"
                placeholder="Enter your email"
                {...register("email")}
              />
              {errors.email ? <p>{errors.email.message}</p>:""}
            </div>
      
            <div className="form-group">
              <label htmlFor="password">Password</label>
              <input
                type="password"
                id="password"
                placeholder="Enter your password"
                {...register("password")}
              />
              {errors.password ? <p>{errors.password.message}</p>:""}
            </div>
      
            <button type="submit">Register</button>
          </form>
        </div>
      );
}

export default RegisterPage