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
	//����һ��buffersID(NumNuffers=1),ͨ����˳���123.������
	//���������BUffers��
	glNamedBufferStorage(Buffers[ArrayBuffer], sizeof(vertices), vertices, 0);
	//��buffers�еĵ�0��ID����һ������ģ�ȡ������ΪĿ��Buffer�������id����ռ䣬����ռ�����ֽ����id
	//�����id����Ŀռ��С �磺6*2*4 ���������Ǵ洢���ڴ棬���һ���Ǳ�ǣ�0�൱��û��
	//׼����opengl�������ݣ������ݻ�������  
	//�����Դ�


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

	//�����ݴ��ݵ�opengl���������ݵ�λ�� ��С ����
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


