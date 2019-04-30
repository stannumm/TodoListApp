export function PostData(type, userData) {
    let BaseURL = 'https://localhost:44351/';

    return new Promise((resolve, reject) =>{
    
         
        fetch(BaseURL+type, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                },
            body: JSON.stringify(userData)
          })
          .then((response) => {
           if(response.ok){
                return response.json();
           }
          })
          .then((res) => {
            resolve(res);
          })
          .catch((error) => {
            reject(error);
          });

  
      });
}