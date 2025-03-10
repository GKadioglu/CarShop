// api.js
export const fetchWithToken = async (url, options = {}) => {
    const token = localStorage.getItem('jwtToken');
    console.log("token",token)
    const headers = {
      ...options.headers,
      'Authorization': `Bearer ${token}`,
    };

    console.log("headers",headers)
  
    const response = await fetch(url, {
      ...options,
      headers,
    });

    console.log("response",response)
  
    return response;
  };