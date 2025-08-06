import react, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { register } from '../Api/auth'

const Register: React.FC = () => {
const[userName ,setUserName]=useState<string>('')
const [email ,setEmail]=useState<string>('')
const [password ,setPassword]=useState<string>('')
const [confirmPassword ,setConfirmPassword]=useState<string>('')
const [role ,setRole]=useState<string>('patient')
const navigate=useNavigate();
const handleSubmit=async(e:React.FormEvent)=>{
    e.preventDefault();
    if (password !== confirmPassword) {
        alert('Passwords do not match');
        return;
      }
      try{
        await register(userName,email,password,confirmPassword,role);//retourne la reponse au api
        navigate('/login');
      }
      catch(error: any){
        alert(error.message || 'Registration failed');
      }
}

return (
    <div className='flex justify-center items-center h-screen bg-gray-100'>
        <form onSubmit={handleSubmit} className='bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4'>
            <h2 className='text-2xl font-bold mb-6'>Register</h2>
            <div className='mb-4'>
                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='userName'>
                    Username
                </label>
                <input
                    type='text'
                    id='userName'
                    className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500'
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />
            </div>
            <div className='mb-4'>
                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='email'>
                    Email
                </label>
                <input
                    type='email'
                    id='email'
                    className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
            </div>
            <div className='mb-4'>
                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='password'>
                    Password
                </label>
                <input
                    type='password'
                    id='password'
                    className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
            </div>
            <div className='mb-4'>
                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='confirmPassword'>
                    Confirm Password
                </label>
                <input
                    type='password'
                    id='confirmPassword'
                    className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500'
                    value={confirmPassword}     
                    onChange={(e) => setConfirmPassword(e.target.value)}
                />
            </div>
            <div className='mb-4'>
                <label className='block text-gray-700 text-sm font-bold mb-2' htmlFor='role'>
                    Role
                </label>
                <select
                    id='role'
                    className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500'
                    value={role}
                    onChange={(e) => setRole(e.target.value)}
                >
                    <option value='patient'>Patient</option>
                    <option value='doctor'>Doctor</option>
                </select>
            </div>
            <button
                type='submit'
                className='bg-indigo-500 hover:bg-indigo-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline'
            >
                Register
            </button>
        </form>



    </div>

    
)
}
export default Register