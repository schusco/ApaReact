import { usePlayers } from '../context/PlayerContext';
import { useFormHandler } from '../hooks/formHandler';
import { Navigate, useNavigate } from 'react-router-dom';
function Settings() {
    const { user, setUser } = usePlayers(); 
    const navigate = useNavigate();
    let initState = { firstName: '', lastName: 0, sl8: 0, sl9: 0, playerNumber: 0 };
    if (user) {
        initState.firstName = user.firstName;
        initState.lastName = user.lastName;
        initState.sl8 = user.sl8;
        initState.sl9 = user.sl9;
        initState.playerNumber = user.playerNumber;
    }
    const handleSave = (result) => {
        setUser(result); 
        navigate('/');
    }

    const { formData: data, handleSubmit, handleChange, isSubmitting } = useFormHandler(initState, '/api/players', handleSave, 'PUT');
    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">First Name</label>
                <div className="col-auto">
                    <input type="text" className=" col-sm-5 form-control" name="firstName" value={data.firstName} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">Last Name</label>
                <div className="col-auto">
                    <input type="text" className=" col-sm-5 form-control" id="inningsInput" name="innings" value={data.lastName} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">8 Ball SL</label>
                <div className="col-auto">
                    <input type="number" className="form-control" id="defensesInput" name="sl8" value={data.sl8} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label className="col-sm-2 col-form-label">9 Ball SL</label>
                <div className="col-auto">
                    <input type="number" className="form-control" name="sl9" value={data.sl9} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <div className="col-sm-2 "></div>
                <div className="col-auto">
                    <button type="submit" disabled={isSubmitting} className="btn btn-info" >
                        {isSubmitting ? 'Saving...' : 'Enter'}</button>
                </div>
            </div>
        </form>
    );
}
export default Settings