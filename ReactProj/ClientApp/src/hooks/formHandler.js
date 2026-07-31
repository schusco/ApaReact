// src/hooks/useFormHandler.js
import { useState } from 'react';
import { usePlayers } from '../context/PlayerContext';

export function useFormHandler(initialState, apiEndpoint, onSuccess, method) {
    const [formData, setFormData] = useState(initialState);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);       // <-- Added error state
    const [success, setSuccess] = useState(false);  // <-- Added success state    
    if (!method) {
        method = 'POST';
    }
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };
      
    // Shared submit handler
    const handleSubmit = async (e) => {        
        e.preventDefault();
        setIsSubmitting(true);
        setError(null);   // Reset error state
        setSuccess(false); // Reset success state
        try {
            const response = await fetch(apiEndpoint, {
                method: method,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Submission failed');
            }
            const result = await response.json();            
            setSuccess(true);  // Set success state on successful submission
            // Reset form on success or handle post-submit logic
            setFormData(initialState);
            if (onSuccess) {                
                onSuccess(result);  // Call the onSuccess callback if provided
            }
        } catch (err) {            
            setError(err.message);  // Set error state
            console.error(err);
        } finally {
            setIsSubmitting(false);
        }
    };

    return { formData, handleChange, handleSubmit, isSubmitting, error, success, setFormData };
}