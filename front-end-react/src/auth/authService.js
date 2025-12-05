export async function callSecureApi(getAccessTokenSilently) {
  const token = await getAccessTokenSilently();

  const response = await fetch("https://suaapi.com/secure", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  return await response.json();
}
