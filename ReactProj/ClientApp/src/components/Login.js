import { usePlayers } from '../context/PlayerContext';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
function Login() {
    const { login } = usePlayers();
    const [playerNumber, setPlayerNumber] = useState('')
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const [isSubmitting, setIsSubmitting] = useState(false);
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            setIsSubmitting(true);
            await login(playerNumber, password);
            navigate('/');
        }
        catch (err) {
            console.error('Login failed', err);
        }
        finally {
            setIsSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h3>Login</h3>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">Player Number</label>
                <div className="col-auto">
                    <input type="number" className=" col-sm-5 form-control" name="playerNumber" value={playerNumber} onChange={(e) => setPlayerNumber(e.target.value)} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">Password</label>
                <div className="col-auto">
                    <input type="password" className=" col-sm-5 form-control" name="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
            </div>
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