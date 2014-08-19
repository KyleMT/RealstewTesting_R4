//
// Assignment 1 startup code for Windows
//
#include <stdio.h>
#include <winsock.h>

// Define the port number to identify this process
#define MYPORT 3490

int main() {
	int s, n;
	unsigned fd;
	struct sockaddr_in my_addr;
	WSADATA wsaData;
	char *header = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\n";
	char data[512];
	char filename[256];
	char buffer[300];
	FILE *f;

	// Initialize windos sockets
	WSAStartup(MAKEWORD(1, 1), &wsaData);

	// Construct address information
	my_addr.sin_family = AF_INET;
	my_addr.sin_port = htons(MYPORT);
	my_addr.sin_addr.s_addr = INADDR_ANY;
	memset(my_addr.sin_zero, '\0', sizeof(my_addr.sin_zero));

	// Create a socket and bind it the port MYPORT
	s = socket(PF_INET, SOCK_STREAM, 0);
	bind(s, (struct sockaddr *)&my_addr, sizeof(my_addr));

	// Allow up to 10 incoming connections
	listen(s, 10);
	printf("test\n");
	while (1) {
		fd = accept(s, NULL, NULL);
		n = recv(fd, data, 512, 0);
		data[n] = 0;                          // NUL terminate it
		//sscanf(data, "GET /%s", filename);
		fopen_s(&f,filename, "rb");
		send(fd, header, strlen(header), 0);
		//
		// send the file
		//
		fread(buffer, 300, 1, f);
		send(fd, buffer, strlen(buffer), 0);
		fclose(f);
		closesocket(fd);                    // close the socket
	}
}
