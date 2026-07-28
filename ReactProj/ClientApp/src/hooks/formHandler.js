// src/hooks/useFormHandler.js
import { useState } from 'react';

export function useFormHandler(initialState, apiEndpoint, onSuccess) {
    const [formData, setFormData] = useState(initialState);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);       // <-- Added error state
    const [success, setSuccess] = useState(false);  // <-- Added success state
    // Shared change handler
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
                method: 'POST',
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
                console.log('running success handler');
                onSuccess(result);  // Call the onSuccess callback if provided
            }
        } catch (err) {            
            setError(err.message);  // Set error state
            console.error(err);
        } finally {
            setIsSubmitting(false);
        }
    };

    return { formData, handleChange, handleSubmit, isSubmitting, error, success };
}