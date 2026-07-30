import { usePlayers } from '../context/PlayerContext';
import { useFormHandler } from '../hooks/formHandler';
import { Navigate } from 'react-router-dom';
export function Players() {
    const handleSave = (player) => {
        setPlayerList(prevList => {
            const updatedList = [...prevList, player];
            return updatedList.sort((a, b) => a.fullName.localeCompare(b.fullName));
        });
    };
    const { setPlayerList, user } = usePlayers();
    const { formData: data, handleChange, handleSubmit, isSubmitting, error, success } = useFormHandler({ playerNumber: '', firstName: '', lastName: '', canScoreFor: false }, '/api/players', handleSave);

    if (!user) {
        return <Navigate to="/login" replace />;
    }

    return (
        <form onSubmit={handleSubmit}>
            {success && (
                <div className="alert alert-success p-2 mb-2">Player added successfully</div>
            )}
            {error && (
                <div className="alert alert-danger mb-2 p-2">{error}</div>
            )}
            <h3>Add New Player</h3>
            <div className="row g-3 mb-3">
                <div className="col-sm-2 text-end">
                    <label className="pt-1">Player Number</label>
                </div>
                <div className="col-auto">
                    <input type="number" className="form-control pt-1" name="playerNumber" value={data.playerNumber} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <div className="col-sm-2 text-end">
                    <label className="pt-1">First Name</label>
                </div>
                <div className="col-auto">
                    <input type="text" className="form-control pt-1" name="firstName" value={data.firstName} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <div className="col-sm-2 text-end">
                    <label className="pt-1">Last Name</label>
                </div>
                <div className="col-auto">
                    <input type="text" className="form-control pt-1" name="lastName" value={data.lastName} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <div className="col-sm-2 text-end">
                    <label className="pt-1">Scorable</label>
                </div>
                <div className="col-auto align-content-center p-1">
                    <input type="checkbox" className="pt-3" name="canScoreFor" checked={data.canScoreFor} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <div className="col-sm-2"></div>
                <div className="col-auto">
                    <button type="submit" disabled={isSubmitting} className="btn btn-info" >
                        {isSubmitting ? 'Saving...' : 'Add Player'}</button>
                </div>
            </div>
        </form >
    );

}