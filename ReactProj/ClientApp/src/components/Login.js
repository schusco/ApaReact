import { usePlayers } from '../context/PlayerContext';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
function Login() {
    const { login } = usePlayers();
    const [playerNumber, setPlayerNumber] = useState('')
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null); 
    const [passwordConfirm, setPasswordConfirm] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [blankPassword, setBlankPassword] = useState(false);
    const navigate = useNavigate();
    const [isSubmitting, setIsSubmitting] = useState(false);
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            setIsSubmitting(true);
            if (!showPassword) {                
                await checkUser(playerNumber);
            }
            else {                
                await login(playerNumber, password, !blankPassword);
                navigate('/');
            }
        }
        catch (err) {
            console.error('Login failed', err);
            setError('Login failed');
        }
        finally {
            setIsSubmitting(false);
        }
    };
    const checkUser = async (playerNumber) => {

        try {
            const response = await fetch('/api/auth/checkUser', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ playerNumber, password })
            });
            if (!response.ok) {
                throw new Error('Login failed');
            }
            const result = await response.json();
            setBlankPassword(result.blankPassword);
            setShowPassword(true);
        } catch (error) {
            console.error("Error checking user: ", error);
        }
    };
    return (
        <form onSubmit={handleSubmit}>
            <h3>Login</h3>
            {!showPassword && (
                <div className="row g-3 mb-3">
                    <label className="col-sm-2 col-form-label">Player Number</label>
                    <div className="col-auto">
                        <input type="number" className=" col-sm-5 form-control" name="playerNumber" value={playerNumber} onChange={(e) => setPlayerNumber(e.target.value)} />
                    </div>
                </div>
            )}
            {showPassword && (
                <div className="row g-3 mb-3">
                    <label className="col-sm-2 col-form-label">Password</label>
                    <div className="col-auto">
                        <input type="password" className=" col-sm-5 form-control" name="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                    </div>
                </div>
            )}
            {showPassword && blankPassword && (
                <div className="row g-3 mb-3">
                    <label className="col-sm-2 col-form-label">Confirm Password</label>
                    <div className="col-auto">
                        <input type="password" className=" col-sm-5 form-control" name="password" value={passwordConfirm} onChange={(e) => setPasswordConfirm(e.target.value)} />
                    </div>
                </div>
            )}
            {error && (
                <div className="alert alert-danger mb-2 p-2">{error}</div>
            )}
            <div className="row g-3 mb-3">
                <div className="col-sm-2 "></div>
                <div className="col-auto">
                    <button type="submit" disabled={isSubmitting} className="btn btn-info" >
                        {isSubmitting ? 'Loading...' : 'Login'}</button>
                </div>
            </div>
        </form>
    )
}
export default Login