using namespace std;

#include "vgl.h"
#include "LoadShaders.h"

enum VAO_IDs { Triangles, NumVAOs };
enum Buffer_IDs { ArrayBuffer, NumBuffers };
enum Attrib_IDs { vPosition = 0 };

GLuint VAOs[NumVAOs];
GLuint Buffers[NumBuffers];

const GLuint NumVertices = 6;

//init
void init(void)
{
	static const GLfloat vertices[NumVertices][2] =
	{
		{-0.90, -0.90}, //Triangle 1
		{0.85,  -0.90},
		{-0.90, 0.85},
		{0.90, -0.85}, //Triangle 2
		{0.90, 0.90},
		{-0.85, 0.90}
	};
	glCreateBuffers(NumBuffers, Buffers);
	//分配一个buffersID(NumNuffers=1),通常安顺序给123.。。，
	//存放在数组BUffers中
	glNamedBufferStorage(Buffers[ArrayBuffer], sizeof(vertices), vertices, 0);
	//把buffers中的第0个ID（上一步分配的）取出来作为目标Buffer，给这个id分配空间，这个空间的名字叫这个id
	//给这个id分配的空间大小 如：6*2*4 ，第三个是存储的内存，最后一个是标记，0相当于没给
	//准备向opengl传递数据，把数据缓存起来  
	//分配显存


	ShaderInfo shaders[] =
	{
		{GL_VERTEX_SHADER, "triangles.vert"},
		{GL_FRAGMENT_SHADER, "triangles.frag"},
		{GL_NONE, NULL}
	};

	GLuint program = LoadShaders(shaders);
	glUseProgram(program);
	glGenVertexArrays(NumVAOs, VAOs);
	glBindVertexArray(VAOs[Triangles]);
	glBindBuffer(GL_ARRAY_BUFFER, Buffers[ArrayBuffer]);
	glVertexAttribPointer(vPosition, 2, GL_FLOAT, GL_FALSE, 0, BUFFER_OFFSET(0));
	gl3wEnableVertexAttribArray(vPosition);

};
//
//display
//
//
void dispaly(void)
{
	static const float black[] = { 0.0f,0.0f,0.0f,0.0f };
	glClearBufferfv(GL_COLOR, 0, black);
	glBindVertexArray(VAOs[Triangles]);
	glDrawArrays(GL_TRIANGLES, 0, NumVertices);
	GLenum error = glGetError();

	//把数据传递到opengl，包括传递的位置 大小 内容
};

//
//main
//
int main(int argc, char** argv)
{
	glfwInit();
	GLFWwindow* window = glfwCreateWindow(640, 480, "Triangles", NULL, NULL);

	glfwMakeContextCurrent(window);
	gl3wInit();

	init();

	while (!glfwWindowShouldClose(window))
	{
		dispaly();
		glfwSwapBuffers(window);
		glfwPollEvents();
	}
	glfwDestroyWindow(window);
	glfwTerminate();
};


